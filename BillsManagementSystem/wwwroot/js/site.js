
$(document).ready(function () {
    $('#billsTable').DataTable();

    $.ajax({
        url: '/BillManagment/getAllVendors',
        type: "Post",
        success: function (data) {
            $('#Vendors').empty();
            var options = '';
            options += '<option value=" ">Select Vendor</option>';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i].value + '">' + data[i].text + '</option>';
            }
            $('#Vendors').append(options);
        }
    });

    $.ajax({
        url: '/BillManagment/GetAllItems',
        type: "Post",
        success: function (data) {
            $('#items').empty();
            var options = '';
            options += '<option value=" ">Select the Item</option>';
            for (var i = 0; i < data.length; i++) {
                options += '<option value="' + data[i].value + '">' + data[i].text + '</option>';
            }
            $('#items').append(options);
        }
    });

    function readFile(input) {
        if (input.files && input.files[0]) {
            var reader = new FileReader();

            reader.onload = function (e) {
                var photo = $("#billImagePreview");
                photo.attr("src", e.target.result)
            }
            reader.readAsDataURL(input.files[0]);
        }
    }

    jQuery(document).ready(function ($) {
        if (window.jQuery().datetimepicker) {
            $('.datetimepicker').datepicker();
        }
    });

    $('#billImage').on('change', function () { readFile(this); });
    $('#items').on('change', function () {
        $.ajax({
            url: '/BillManagment/GetItemPriceByCode/' + this.value,
            type: "Get",
            success: function (data) {
                console.log(data);
                $('#itemPrice').val(data);
            }
        });
    });

    function PrepareBillDetails() {
        billDetail.BILCOD = $('#billCode').val();
        billDetail.ITMCOD = $('#items').val();
        billDetail.ITMPRC = $('#itemPrice').val();
        billDetail.ITMQTY = $('#itemQuantity').val();
    }

    $("#finish").click(function () {
        $.ajax({
            url: '/BillManagment/GetBillDetailByBillCode/' + billDetail.BILCOD,
            type: "Get",
            success: function (data) {
                var table = ""
                console.log(data)
                for (var i = 0; i < data.length; i++) {
                    table += `<tr>
                    <th scope="row" id="itemName">${data[i]['itmcodNavigation']['itmnam']}</th>
                    <td id="itemPrice">${data[i]['itmprc']}</td>
                    <td id="itemQ">${data[i]['itmqty']}</td>
                    <td>${data[i]['itmprc'] * data[i]['itmqty']}</td>
               </tr>`;
                }
                Swal.fire({
                    title: 'All done!',
                    html: `
        <div class="container" style="width:100%;>
    <div class="row">
        <div class="col-sm-6  col-md">
            <h3 class="pull-left">Bill Price: ${data[0]['bilcodNavigation']['bilprc']}</h3>
        </div>
        <div class="col-sm-6  col-md">
            <h3 class="text-lg-right">Bill Code: ${data[0]['bilcod']}</h3>
            <p class="pull-right">Bill date: ${data[0]['bilcodNavigation']['bildat']}</p>
        </div>
    </div>
    <div class="row">
        <div class="col-sm  col-md">
            <table class="table" id="previewTable">
                <thead>
                    <tr>
                        <th scope="col">Item name</th>
                        <th scope="col">Item price</th>
                        <th scope="col">Quantity</th>
                        <th scope="col">Total</th>
                    </tr>
                </thead>
                <tbody>
                    ${table}
                </tbody>
            </table>
        </div>
    </div>
    <div class="row">
        <div class="col-sm"></div>
        <div class="col-sm"></div>
        <div class="col-sm"></div>
        <div class="col-sm">Total: ${data[0]['bilcodNavigation']['bilprc']}</div>

    </div>
</div>
      `,
                    confirmButtonText: 'Done'
                })
            }
        });

    })



    function EmptyBillDetail() {
        $('#items').val(" ");
        $('#itemPrice').val("");
        $('#itemQuantity').val("");
    }

    function ValidateStepTwo() {
        isValid = false;
        let item = $('#items').val();
        let itemQuantity = $('#itemQuantity').val();
        if (item == " " && itemQuantity == "") {

            $("#itemQuantity_span").removeClass("hide");
            $("#items_span").removeClass("hide");
            return;
        } else {
            $("#itemQuantity_span").addClass("hide");
            $("#items_span").addClass("hide");
        }
        if (item == " ") {
            $("#items_span").removeClass("hide");
        } else if (itemQuantity == "") {
            $("#itemQuantity_span").removeClass("hide");
        }
        else {
            $("#items_span").addClass("hide");
            $("#itemQuantity_span").addClass("hide");
            isValid = true;
        }
    }

    $("#addItem").click(function () {
        ValidateStepTwo();
        if (isValid) {
            PrepareBillDetails();
            $.ajax({
                url: '/BillManagment/InsertBillDetails/' + billDetail.BILCOD,
                type: "Post",
                data: { billDetail: billDetail },
                success: function (data) {
                    EmptyBillDetail();
                    Swal.fire(
                        'The item has been added',
                        '',
                        'success'
                    )
                    $.ajax({
                        url: '/BillManagment/UpdateBillsList',
                        type: "Get",
                        success: function (data) {
                            $('#billList').empty();
                            $('#billList').html(data);
                            $('#billsTable').DataTable();
                        }
                    });
                }
            });
        }
    })

});

function EditBill(billCode) {
    $.ajax({
        url: '/BillManagment/EditBill/' + billCode,
        type: "Get",
        success: function (data) {
            $('#cardHead').empty();
            $('#cardHead').html(data);
        }
    });
}

function DeleteBill(billCode) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: '/BillManagment/deleteBillByBillCode/' + billCode,
                type: "Delete",
                success: function (data) {
                    if (data == "Success") {
                        Swal.fire(
                            'The Item has been deleted successfully',
                            '',
                            'success'
                        )
                        $.ajax({
                            url: '/BillManagment/UpdateBillsList',
                            type: "Get",
                            success: function (data) {
                                $('#billList').empty();
                                $('#billList').html(data);
                                $('#billsTable').DataTable();
                            }
                        });
                    } else {
                        Swal.fire(
                            '',
                            'The item Not deleted ',
                            'fail'
                        )
                    }
                }
            });
        }
    })

   
}

function EditItemRow(id) {
    $.ajax({
        url: '/BillManagment/GetBillItemByItemCode/' + id,
        type: "Get",
        success: function (data) {
            console.log(data);
            Swal.fire({
                title: '<strong>Edit Item</u></strong>',
                icon: 'info',
                html:
                    `   <div class="panel panel-primary setup-content" id="step-2">
                    <div class="panel-heading">
                        <h3 class="panel-title">Bill Details</h3>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <label class="control-label">Bill Code</label>
                                    <input maxlength="100" disabled type="text" required="required" id="billCode" class="form-control" value="${data.BILCOD}" />
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <label for="items">Choose an item:</label>
                                    <select id="items" class="form-control">
                                    </select>
                                    <span class="hide text-danger" id="items_span">Please select the item</span>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm">
                                <div class="form-group">
                                    <label class="control-label">Item Price</label>
                                    <input maxlength="100" disabled type="text" required="required" class="form-control" id="itemPrice" value="${data.ITMPRC}" />
                                </div>
                            </div>
                            <div class="col-sm">
                                <div class="form-group">
                                    <label class="control-label">Item Quantity</label>
                                    <input maxlength="100" type="number" required="required" id="itemQuantity" class="form-control" value="${data.ITMQTY}" />
                                    <span class="hide text-danger" id="itemQuantity_span">Please enter the item quantity </span>
                                </div>
                            </div>
                        </div>
                        <button class="btn btn-success pull-right">Save</button>
                    </div>
                </div>`,
                showCloseButton: true,
                showCancelButton: true,
                focusConfirm: false,
                confirmButtonText:
                    'Save',
                confirmButtonAriaLabel: 'Thumbs up, great!'
            })
        }
    });
   
}

var isValid = true;
var billHeader = {};
var billDetail = {};
$(document).ready(function () {
    function ValidateStepOne() {
        debugger
        isValid = false;
        let vendor = $('#Vendors').val();
        let billDate = $('#billDate').val();
        if (vendor == " " && billDate == "") {

            $("#Vendors_span").removeClass("hide");
            $("#billDate_span").removeClass("hide");
            return;
        } else {
            $("#Vendors_span").addClass("hide");
            $("#billDate_span").addClass("hide");
        }
        if (vendor == " ") {
            $("#Vendors_span").removeClass("hide");
        } else if (billDate == "") {
            $("#billDate_span").removeClass("hide");
        }
        else {
            $("#Vendors_span").addClass("hide");
            $("#billDate_span").addClass("hide");
            isValid = true;
            $('#step1Icon').attr("href", "");
        }

    }
    function PrepareBillHeaderObject() {
        billHeader.BILDAT = $('#billDate').val();
        billHeader.VNDCOD = $('#Vendors').val();
        billHeader.BILIMG = $('#billImage').val();
    }


    var navListItems = $('div.setup-panel div a'),
        allWells = $('.setup-content'),
        allNextBtn = $('.nextBtn');

    allWells.hide();

    navListItems.click(function (e) {
        e.preventDefault();
        var $target = $($(this).attr('href')),
            $item = $(this);

        if (!$item.hasClass('disabled')) {
            navListItems.removeClass('btn-success').addClass('btn-default');
            $item.addClass('btn-success');
            allWells.hide();
            $target.show();
            $target.find('input:eq(0)').focus();
        }
    });

    allNextBtn.click(function () {
        debugger
        var curStep = $(this).closest(".setup-content"),
            curStepBtn = curStep.attr("id"),
            nextStepWizard = $('div.setup-panel div a[href="#' + curStepBtn + '"]').parent().next().children("a"),
            curInputs = curStep.find("input[type='text'],input[type='url']")
        //isValid = true;
        ValidateStepOne();
        $(".form-group").removeClass("has-error");
        for (var i = 0; i < curInputs.length; i++) {
            if (!curInputs[i].validity.valid) {
                isValid = false;
                $(curInputs[i]).closest(".form-group").addClass("has-error");
            }
        }

        if (isValid) {
            nextStepWizard.removeAttr('disabled').trigger('click')
            PrepareBillHeaderObject();
            $.ajax({
                url: '/BillManagment/InsertBillHeader',
                type: "Post",
                data: { billHeader: billHeader },
                success: function (data) {
                    $('#billCode').val(data);
                }
            });
        };
    });

    $('div.setup-panel div a.btn-success').trigger('click');
});