$('input#item_Visible').change(function () {
    comId = $(this).prev().val();
    isChecked = $(this).is(':checked');
    $.ajax({
        url: '/Admin/comment/Save',
        type: "POST",
        data: { id: comId, isChecked: isChecked },
        dataType: "application/json; charset=utf-8"
    });
});
