// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Site.js - JavaScript chính cho website bán xe

// Khởi tạo khi DOM đã load
$(document).ready(function () {
    // Khởi tạo các component
    initializeComponents();

    // Xử lý sự kiện
    bindEvents();
});

// Khởi tạo các component
function initializeComponents() {
    // Khởi tạo carousel cho hình ảnh xe
    initializeCarousel();

    // Khởi tạo tooltip
    initializeTooltips();

    // Khởi tạo dropdown
    initializeDropdowns();

    // Format số tiền
    formatPriceDisplay();
}

// Khởi tạo carousel
function initializeCarousel() {
    $('.car-carousel').each(function () {
        $(this).owlCarousel({
            loop: true,
            margin: 10,
            nav: true,
            responsive: {
                0: { items: 1 },
                600: { items: 2 },
                1000: { items: 3 }
            }
        });
    });
}

// Khởi tạo tooltips
function initializeTooltips() {
    $('[data-toggle="tooltip"]').tooltip();
}

// Khởi tạo dropdowns
function initializeDropdowns() {
    $('.dropdown-toggle').dropdown();
}

// Format hiển thị giá tiền
function formatPriceDisplay() {
    $('.price-display').each(function () {
        var price = $(this).text();
        var formattedPrice = formatCurrency(price);
        $(this).text(formattedPrice);
    });
}

// Format số thành tiền tệ
function formatCurrency(amount) {
    return new Intl.NumberFormat('vi-VN', {
        style: 'currency',
        currency: 'VND'
    }).format(amount);
}

// Bind các sự kiện
function bindEvents() {
    // Xử lý click vào card xe
    bindCarCardClick();

    // Xử lý nút yêu thích
    bindFavoriteButton();

    // Xử lý nút chia sẻ
    bindShareButton();

    // Xử lý form liên hệ
    bindContactForm();

    // Xử lý scroll để load thêm
    bindInfiniteScroll();
}

// Xử lý click vào card xe
function bindCarCardClick() {
    $('.car-card').on('click', function (e) {
        if (!$(e.target).hasClass('btn') && !$(e.target).closest('.btn').length) {
            var carId = $(this).data('car-id');
            if (carId) {
                window.location.href = '/Cars/Details/' + carId;
            }
        }
    });
}

// Xử lý nút yêu thích
function bindFavoriteButton() {
    $('.btn-favorite').on('click', function (e) {
        e.stopPropagation();
        var $btn = $(this);
        var carId = $btn.data('car-id');

        // Toggle trạng thái
        $btn.toggleClass('active');

        // Gửi request đến server (nếu cần)
        $.ajax({
            url: '/Cars/ToggleFavorite',
            method: 'POST',
            data: { carId: carId },
            success: function (response) {
                if (response.success) {
                    showToast('Đã ' + (response.isFavorite ? 'thêm vào' : 'xóa khỏi') + ' danh sách yêu thích');
                }
            },
            error: function () {
                // Revert trạng thái nếu lỗi
                $btn.toggleClass('active');
                showToast('Có lỗi xảy ra, vui lòng thử lại', 'error');
            }
        });
    });
}

// Xử lý nút chia sẻ
function bindShareButton() {
    $('.btn-share').on('click', function (e) {
        e.stopPropagation();
        var carId = $(this).data('car-id');
        var url = window.location.origin + '/Cars/Details/' + carId;

        if (navigator.share) {
            navigator.share({
                title: 'Xe ô tô đang bán',
                url: url
            });
        } else {
            // Fallback: copy to clipboard
            copyToClipboard(url);
            showToast('Đã copy link vào clipboard');
        }
    });
}

// Xử lý form liên hệ
function bindContactForm() {
    $('.contact-form').on('submit', function (e) {
        e.preventDefault();

        var $form = $(this);
        var formData = $form.serialize();

        $.ajax({
            url: $form.attr('action'),
            method: 'POST',
            data: formData,
            beforeSend: function () {
                $form.find('button[type="submit"]').prop('disabled', true);
            },
            success: function (response) {
                if (response.success) {
                    showToast('Gửi thông tin thành công! Chúng tôi sẽ liên hệ với bạn sớm.');
                    $form[0].reset();
                } else {
                    showToast(response.message || 'Có lỗi xảy ra', 'error');
                }
            },
            error: function () {
                showToast('Có lỗi xảy ra, vui lòng thử lại', 'error');
            },
            complete: function () {
                $form.find('button[type="submit"]').prop('disabled', false);
            }
        });
    });
}

// Xử lý infinite scroll
function bindInfiniteScroll() {
    var isLoading = false;
    var currentPage = 1;
    var hasMore = true;

    $(window).on('scroll', function () {
        if (isLoading || !hasMore) return;

        if ($(window).scrollTop() + $(window).height() >= $(document).height() - 100) {
            loadMoreCars();
        }
    });

    function loadMoreCars() {
        isLoading = true;
        currentPage++;

        $.ajax({
            url: '/Cars/LoadMore',
            method: 'GET',
            data: { page: currentPage },
            success: function (response) {
                if (response.cars && response.cars.length > 0) {
                    $('.cars-container').append(response.html);
                    hasMore = response.hasMore;
                } else {
                    hasMore = false;
                }
            },
            error: function () {
                currentPage--; // Revert page number
            },
            complete: function () {
                isLoading = false;
            }
        });
    }
}

// Utility functions
function showToast(message, type = 'success') {
    var toastClass = type === 'error' ? 'toast-error' : 'toast-success';
    var toast = $('<div class="toast ' + toastClass + '">' + message + '</div>');

    $('body').append(toast);

    setTimeout(function () {
        toast.addClass('show');
    }, 100);

    setTimeout(function () {
        toast.removeClass('show');
        setTimeout(function () {
            toast.remove();
        }, 300);
    }, 3000);
}

function copyToClipboard(text) {
    var textArea = document.createElement("textarea");
    textArea.value = text;
    document.body.appendChild(textArea);
    textArea.select();
    document.execCommand('copy');
    document.body.removeChild(textArea);
}

// Xử lý loading state
function showLoading() {
    $('.loading-overlay').show();
}

function hideLoading() {
    $('.loading-overlay').hide();
}