$(document).ready(function () {
    var orderItems = [];
    //Add button click function
    $('#add').click(function () {
        //Check validation of order item
        var isValidItem = true;

        if ($('#productId').val().trim() == '') {
            isValidItem = false;
            $('#productId').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#productId').siblings('span.error').css('visibility', 'hidden');
        }

        if ($('#itemName').val().trim() == '') {
            isValidItem = false;
            $('#itemName').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#itemName').siblings('span.error').css('visibility', 'hidden');
        }
 
        if (!($('#quantity').val().trim() != '' && !isNaN($('#quantity').val().trim()))) {
            isValidItem = false;
            $('#quantity').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#quantity').siblings('span.error').css('visibility', 'hidden');
        }
 
        if (!($('#rate').val().trim() != '' && !isNaN($('#rate').val().trim()))) {
            isValidItem = false;
            $('#rate').siblings('span.error').css('visibility', 'visible');
        }
        else {
            $('#rate').siblings('span.error').css('visibility', 'hidden');
        }
 
        //Add item to list if valid
        if (isValidItem) {
            orderItems.push({
                ItemName: $('#itemName').val().trim(),
                Quantity: parseInt($('#quantity').val().trim()),
                Rate: parseFloat($('#rate').val().trim()),
                TotalAmount: parseInt($('#quantity').val().trim()) * parseFloat($('#rate').val().trim())
            });
            var d = parseInt(document.getElementById('t').value)

            var c = parseInt($('#quantity').val().trim()) * parseFloat($('#rate').val().trim());

            d += c;
            document.getElementById('t').value = d;

           
            
            //Clear fields
            $('#productId').val('').focus();
            $('#quantity,#rate,#itemName').val('');
            
           
           
 
        }
        //populate order items
        GeneratedItemsTable();
 
    });

    
    //Save button click function
    $('#submit').click(function () {
        //validation of order
        var isAllValid = true;
        if (orderItems.length == 0) {
            $('#orderItems').html('<span style="color:red;">Please add order items</span>');
            isAllValid = false;
        }
 
    });
    //function for show added items in table
    function GeneratedItemsTable() {
        if (orderItems.length > 0)
        {
            var $table = $('<table/>');
            $table.append('<thead><tr><th>Name</th><th>Quantity</th><th>Unit Price</th><th>Total Price</th><th>Option</th></tr></thead>');
            var $tbody = $('<tbody/>');
            $.each(orderItems, function (i, val) {
                var $row = $('<tr/>');
                $row.append($('<td/>').html(val.ItemName));
                $row.append($('<td/>').html(val.Quantity));
                $row.append($('<td/>').html(val.Rate));
                $row.append($('<td/>').html(val.TotalAmount));
                
                var $remove = $('<a href="#">Remove</a>');
                $remove.click(function (e) {
                    e.preventDefault();
                    orderItems.splice(i, 1);
                    GeneratedItemsTable();
                });
                $row.append($('<td/>').html($remove));
                $tbody.append($row);
            });
            console.log("current", orderItems);
            $table.append($tbody);
            $('#orderItems').html($table);
        }
        else {
            $('#orderItems').html('');
        }
    }
});