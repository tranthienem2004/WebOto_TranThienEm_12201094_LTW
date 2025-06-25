// car-search.js - JavaScript cho chức năng tìm kiếm xe

$(document).ready(function () {
    initializeSearchForm();
    bindSearchEvents();
});

// Khởi tạo form tìm kiếm
function initializeSearchForm() {
    // Khởi tạo slider cho giá và năm
    initializePriceSlider();
    initializeYearSlider();

    // Load dữ liệu cho dropdown
    loadBrands();
    loadCategories();

    // Khởi tạo autocomplete cho location
    initializeLocationAutocomplete();
}

// Khởi tạo slider cho giá
function initializePriceSlider() {
    $("#price-slider").slider({
        range: true,
        min: 0,
        max: 5000,
        step: 50,
        values: [0, 5000],
        slide: function (event, ui) {
            $("#price-from").val(ui.values[0]);
            $("#price-to").val(ui.values[1]);
            $("#price-display").text(ui.values[0] + " - " + ui.values[1] + " triệu");
        }
    });
}

// Khởi tạo slider cho năm
function initializeYearSlider() {
    var currentYear = new Date().getFullYear();
    $("#year-slider").slider({
        range: true,
        min: 2000,
        max: currentYear,
        step: 1,
        values: [2010, currentYear],
        slide: function (event, ui) {
            $("#year-from").val(ui.values[0]);
            $("#year-to").val(ui.values[1]);
            $("#year-display").text(ui.values[0] + " - " + ui.values[1]);
        }
    });
}

// Load danh sách hãng xe
function loadBrands() {
    $.ajax({
        url: '/Cars/GetBrands',
        method: 'GET',
        success: function (brands) {
            var brandSelect = $('#brand-select');
            brandSelect.empty().append('<option value="">Tất cả hãng xe</option>');

            brands.forEach(function (brand) {
                brandSelect.append('<option value="' + brand.Id + '">' + brand.Name + '</option>');
            });
        }
    });
}

// Load danh sách loại xe
function loadCategories() {
    $.ajax({
        url: '/Cars/GetCategories',
        method: 'GET',
        success: function (categories) {
            var categorySelect = $('#category-select');
            categorySelect.empty().append('<option value="">Tất cả loại xe</option>');

            categories.forEach(function (category) {
                categorySelect.append('<option value="' + category.Id + '">' + category.Name + '</option>');
            });
        }
    });
}

// Khởi tạo autocomplete cho location
function initializeLocationAutocomplete() {
    var vietnamProvinces = [
        "Hà Nội", "TP Hồ Chí Minh", "Đà Nẵng", "Hải Phòng", "Cần Thơ",
        "An Giang", "Bà Rịa - Vũng Tàu", "Bắc Giang", "Bắc Kạn", "Bạc Liêu",
        "Bắc Ninh", "Bến Tre", "Bình Định", "Bình Dương", "Bình Phước",
        "Bình Thuận", "Cà Mau", "Cao Bằng", "Đắk Lắk", "Đắk Nông",
        "Điện Biên", "Đồng Nai", "Đồng Tháp", "Gia Lai", "Hà Giang",
        "Hà Nam", "Hà Tĩnh", "Hải Dương", "Hậu Giang", "Hòa Bình",
        "Hưng Yên", "Khánh Hòa", "Kiên Giang", "Kon Tum", "Lai Châu",
        "Lâm Đồng", "Lạng Sơn", "Lào Cai", "Long An", "Nam Định",
        "Nghệ An", "Ninh Bình", "Ninh Thuận", "Phú Thọ", "Phú Yên",
        "Quảng Bình", "Quảng Nam", "Quảng Ngãi", "Quảng Ninh", "Quảng Trị",
        "Sóc Trăng", "Sơn La", "Tây Ninh", "Thái Bình", "Thái Nguyên",
        "Thanh Hóa", "Thừa Thiên Huế", "Tiền Giang", "Trà Vinh", "Tuyên Quang",
        "Vĩnh Long", "Vĩnh Phúc", "Yên Bái"
    ];

    $("#location-input").autocomplete({
        source: vietnamProvinces,
        minLength: 2
    });
}

// Bind các sự kiện tìm kiếm
function bindSearchEvents() {
    // Xử lý submit form tìm kiếm
    $('#search-form').on('submit', function (e) {
        e.preventDefault();
        performSearch();
    });

    // Xử lý tìm kiếm theo thời gian thực
    $('#keyword-input').on('input', debounce(function () {
        if ($(this).val().length >= 2) {
            performQuickSearch();
        }
    }, 500));

    // Xử lý thay đổi filter
    $('.filter-control').on('change', function () {
        performSearch();
    });

    // Xử lý reset form
    $('#reset-search').on('click', function () {
        resetSearchForm();
    });

    // Xử lý sort
    $('#sort-select').on('change', function () {
        performSearch();
    });

    // Xử lý view mode (grid/list)
    $('.view-mode-btn').on('click', function () {
        var mode = $(this).data('mode');
        toggleViewMode(mode);
    });
}

// Thực hiện tìm kiếm
function performSearch() {
    var searchData = {
        keyword: $('#keyword-input').val(),
        brandId: $('#brand-select').val(),
        categoryId: $('#category-select').val(),
        yearFrom: $('#year-from').val(),
        yearTo: $('#year-to').val(),
        priceFrom: $('#price-from').val(),
        priceTo: $('#price-to').val(),
        fuelType: $('#fuel-type-select').val(),
        transmission: $('#transmission-select').val(),
        location: $('#location-input').val(),
        sortBy: $('#sort-select').val(),
        pageNumber: 1
    };

    showLoading();

    $.ajax({
        url: '/Cars/Search',
        method: 'GET',
        data: searchData,
        success: function (response) {
            updateSearchResults(response);
            updatePagination(response);
            updateSearchSummary(response);
        },
        error: function () {
            showToast('Có lỗi xảy ra khi tìm kiếm', 'error');
        },
        complete: function () {
            hideLoading();
        }
    });
}

// Tìm kiếm nhanh (suggestions)
function performQuickSearch() {
    var keyword = $('#keyword-input').val();

    $.ajax({
        url: '/Cars/QuickSearch',
        method: 'GET',
        data: { keyword: keyword },
        success: function (suggestions) {
            showSearchSuggestions(suggestions);
        }
    });
}

// Hiển thị gợi ý tìm kiếm
function showSearchSuggestions(suggestions) {
    var suggestionHtml = '';
    suggestions.forEach(function (item) {
        suggestionHtml += '<div class="suggestion-item" data-value="' + item.name + '">' +
            '<img src="' + item.imageUrl + '" alt="' + item.name + '">' +
            '<span>' + item.name + '</span>' +
            '<small>' + item.brandName + ' - ' + item.formattedPrice + '</small>' +
            '</div>';
    });

    $('#search-suggestions').html(suggestionHtml).show();

    // Xử lý click vào suggestion
    $('.suggestion-item').on('click', function () {
        var value = $(this).data('value');
        $('#keyword-input').val(value);
        $('#search-suggestions').hide();
        performSearch();
    });
}

// Cập nhật kết quả tìm kiếm
function updateSearchResults(response) {
    $('#search-results').html(response.html);

    // Re-bind events cho các element mới
    bindCarCardClick();
    bindFavoriteButton();
}

// Cập nhật pagination
function updatePagination(response) {
    var paginationHtml = '';

    if (response.totalPages > 1) {
        paginationHtml += '<nav aria-label="Search results pagination">';
        paginationHtml += '<ul class="pagination justify-content-center">';

        // Previous button
        if (response.pageNumber > 1) {
            paginationHtml += '<li class="page-item"><a class="page-link" href="#" data-page="' + (response.pageNumber - 1) + '">Trước</a></li>';
        }

        // Page numbers
        var startPage = Math.max(1, response.pageNumber - 2);
        var endPage = Math.min(response.totalPages, response.pageNumber + 2);

        for (var i = startPage; i <= endPage; i++) {
            var activeClass = i === response.pageNumber ? 'active' : '';
            paginationHtml += '<li class="page-item ' + activeClass + '"><a class="page-link" href="#" data-page="' + i + '">' + i + '</a></li>';
        }

        // Next button
        if (response.pageNumber < response.totalPages) {
            paginationHtml += '<li class="page-item"><a class="page-link" href="#" data-page="' + (response.pageNumber + 1) + '">Sau</a></li>';
        }

        paginationHtml += '</ul></nav>';
    }

    $('#pagination-container').html(paginationHtml);

    // Bind pagination events
    $('.page-link').on('click', function (e) {
        e.preventDefault();
        var page = $(this).data('page');
        goToPage(page);
    });
}

// Chuyển trang
function goToPage(page) {
    // Update form data với page mới
    performSearchWithPage(page);
}

// Thực hiện tìm kiếm với page cụ thể
function performSearchWithPage(page) {
    var searchData = getSearchFormData();
    searchData.pageNumber = page;

    showLoading();

    $.ajax({
        url: '/Cars/Search',
        method: 'GET',
        data: searchData,
        success: function (response) {
            updateSearchResults(response);
            updatePagination(response);
            updateSearchSummary(response);

            // Scroll to top of results
            $('html, body').animate({
                scrollTop: $('#search-results').offset().top - 100
            }, 500);
        },
        error: function () {
            showToast('Có lỗi xảy ra khi tìm kiếm', 'error');
        },
        complete: function () {
            hideLoading();
        }
    });
}

// Lấy dữ liệu từ form tìm kiếm
function getSearchFormData() {
    return {
        keyword: $('#keyword-input').val(),
        brandId: $('#brand-select').val(),
        categoryId: $('#category-select').val(),
        yearFrom: $('#year-from').val(),
        yearTo: $('#year-to').val(),
        priceFrom: $('#price-from').val(),
        priceTo: $('#price-to').val(),
        fuelType: $('#fuel-type-select').val(),
        transmission: $('#transmission-select').val(),
        location: $('#location-input').val(),
        sortBy: $('#sort-select').val()
    };
}

// Cập nhật thông tin tóm tắt tìm kiếm
function updateSearchSummary(response) {
    var summaryText = 'Tìm thấy ' + response.totalCars + ' xe phù hợp';
    $('#search-summary').text(summaryText);
}

// Reset form tìm kiếm
function resetSearchForm() {
    $('#search-form')[0].reset();
    $('#price-slider').slider('values', [0, 5000]);
    $('#year-slider').slider('values', [2010, new Date().getFullYear()]);
    $('#search-suggestions').hide();
    performSearch();
}

// Toggle view mode
function toggleViewMode(mode) {
    $('.view-mode-btn').removeClass('active');
    $('[data-mode="' + mode + '"]').addClass('active');

    var container = $('#search-results');
    container.removeClass('grid-view list-view').addClass(mode + '-view');

    // Lưu preference vào localStorage
    localStorage.setItem('viewMode', mode);
}

// Debounce function
function debounce(func, wait) {
    let timeout;
    return function executedFunction(...args) {
        const later = () => {
            clearTimeout(timeout);
            func(...args);
        };
        clearTimeout(timeout);
        timeout = setTimeout(later, wait);
    };
}

// Ẩn suggestions khi click outside
$(document).on('click', function (e) {
    if (!$(e.target).closest('#keyword-input, #search-suggestions').length) {
        $('#search-suggestions').hide();
    }
});

// Load view mode từ localStorage
$(document).ready(function () {
    var savedViewMode = localStorage.getItem('viewMode') || 'grid';
    toggleViewMode(savedViewMode);
});