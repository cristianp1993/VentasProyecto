$(document).ready(function () {
    $('#category-button').on('click', function () {
        $('#category-list').toggleClass('d-none');
    });

    $(document).on('click', function (e) {
        if (!$(e.target).closest('#category-button, #category-list').length) {
            $('#category-list').addClass('d-none');
        }
    });

    $('#category-list input[type="checkbox"]').on('change', function () {
        let  selectedCategories = [];
        let  selectedCategoryNames = [];

        $('#category-list input[type="checkbox"]:checked').each(function () {
            selectedCategories.push($(this).val());
            selectedCategoryNames.push($(this).data('name'));
        });

        $.ajax({
            url: '/Home/Index',
            type: 'GET',
            data: { selectedCategories: selectedCategories.join(',') },
            success: function (data) {
                let  newHtml = $(data).find('#product-container').html();
                $('#product-container').html(newHtml);
            },
            error: function (xhr, status, error) {
                console.error(error);
            }
        });

        updateFilterButtonText(selectedCategoryNames);
    });

    function updateFilterButtonText(selectedCategoryNames) {
        if (selectedCategoryNames.length === 0) {
            $('#category-button').html('<span class="material-symbols-outlined" style="font-let iation-settings:normal">filter_alt</span>Sin filtros aplicados');
        } else if (selectedCategoryNames.length <= 2) {
            $('#category-button').html('<span class="material-symbols-outlined" style="font-let iation-settings:normal">filter_alt</span>' + selectedCategoryNames.join(', '));
        } else {
            $('#category-button').html('<span class="material-symbols-outlined" style="font-let iation-settings:normal">filter_alt</span>' + selectedCategoryNames.length + ' filtros aplicados');
        }
    }
});
