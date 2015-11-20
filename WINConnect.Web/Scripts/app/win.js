$(function () {
    $('a.showhide').click(function () {
        $(this).parents('tr').next('tr').toggleClass('hide');
    });

    $('a.openmodal').click(function (e) {
        e.preventDefault();
        $('div.modal-body').html('<i class="fa fa-spinner"></i>');
        $('div.modal-body').load($(this).attr('data-url'));
    });

    // Datepick
    $('#fromDate, #toDate').datepicker({});
    //IPagedList
    $('div.pagination-container ul.pagination li a').click(function (e) {
        e.preventDefault();
        if (!$(this).parent('li').hasClass('disabled')) {
            $('form').attr('action', $(this).attr('href'));
            $('form').submit();
        }
    });

    // form search button
    $('button#search').click(function (e) {
        e.preventDefault();

        $('form').attr('action', $(this).attr('data-action'));
        $('form').submit();
    });

    // mreq
    $('a.mreq').click(function (e) {
        e.preventDefault();
        $(this).next('div.mreq_result').load($(this).attr('data-url')).show().delay(3000).fadeOut();
    });

    // remove-input-value fields
    //$("input.hasclear").bind("change keyup", function () {
    //    var t = $(this);
    //    t.next('span.clearer').toggle(Boolean(t.val()));
    //});
    //$("span.clearer").hide($(this).prev('input').val());
    //$("span.clearer").click(function () {
    //    $(this).prev('input.hasclear').val('').focus();
    //    $(this).hide();
    //});

    // Sortable table header
    $('table.table thead tr th a').click(function () {
        var sort = $(this).parent('th').attr('data-sort');
        var sortdir = $('#sortdir').val();
        if (sort != '') {
            sortdir = (sortdir == 'desc') ? 'asc' : 'desc';
            $('#sort').val(sort);
            $('#sortdir').val(sortdir);
            $('form').submit();
        }
    });
    /*
    $('#AgentId,#Airline,#Country').change(function () {
        $('#MAWBNumber').val('');
        $('#page').val(1);
        $('#submit').click();
    });
    
    $('#eAwb').change(function () {
        $('#MAWBNumber').val('');
        $('#page').val(1);
        $('#submit').click();
    });
    $("#AgentId-").change(function () {
        var selectedItem = $(this).val();
        var ddlAirlines = $("#Airline");
        var statesProgress = $("#states-loading-progress");
        statesProgress.show();
        $.ajax({
            cache: false,
            type: "GET",
            url: "/api/agents",
            dataType: "json",
            // data: { "AgentId": selectedItem },
            success: function (data) {
                ddlAirlines.html('');
                data = JSON.parse(data);
                alert(data);
                $.each(data, function (k, v) {
                    alert(v.AgentId);
                    ddlAirlines.append($("<option></option>").val(v.AgentId).html(v.AgentName));
                });
                statesProgress.hide();
            },
            error: function (xhr, ajaxOptions, thrownError) {
                alert('Failed to retrieve states.' + thrownError);
                statesProgress.hide();
            }
        });
    });
    */
});
