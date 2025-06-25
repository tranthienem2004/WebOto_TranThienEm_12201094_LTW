document.addEventListener('DOMContentLoaded', function () {
    // Animation cho form khi load
    const loginContainer = document.querySelector('.login-container');
    if (loginContainer) {
        loginContainer.style.opacity = '0';
        loginContainer.style.transform = 'translateY(50px)';

        setTimeout(() => {
            loginContainer.style.transition = 'all 0.8s cubic-bezier(0.4, 0, 0.2, 1)';
            loginContainer.style.opacity = '1';
            loginContainer.style.transform = 'translateY(0)';
        }, 100);
    }

    // Xử lý form submission
    const loginForm = document.querySelector('#loginForm');
    const loginBtn = document.querySelector('.btn-login');

    if (loginForm && loginBtn) {
        loginForm.addEventListener('submit', function (e) {
            // Thêm loading state
            loginBtn.innerHTML = '<span>Đang đăng nhập...</span>';
            loginBtn.disabled = true;

            // Thêm class loading
            loginBtn.classList.add('loading');

            // Reset sau 3 giây nếu không redirect
            setTimeout(() => {
                if (loginBtn.disabled) {
                    loginBtn.innerHTML = 'Đăng Nhập';
                    loginBtn.disabled = false;
                    loginBtn.classList.remove('loading');
                }
            }, 3000);
        });
    }

    // Validation cho email
    const emailInput = document.querySelector('input[type="email"]');
    if (emailInput) {
        emailInput.addEventListener('blur', function () {
            const email = this.value;
            const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

            if (email && !emailRegex.test(email)) {
                this.style.borderColor = '#e74c3c';
                this.style.boxShadow = '0 0 0 3px rgba(231, 76, 60, 0.1)';

                // Remove error styling after user starts typing
                this.addEventListener('input', function () {
                    this.style.borderColor = '#e8ecf0';
                    this.style.boxShadow = 'none';
                }, { once: true });
            }
        });
    }

    // Hiệu ứng cho các input fields
    const inputs = document.querySelectorAll('.form-control');
    inputs.forEach(input => {
        // Thêm focus effect
        input.addEventListener('focus', function () {
            this.parentElement.classList.add('focused');
        });

        input.addEventListener('blur', function () {
            this.parentElement.classList.remove('focused');
        });
    });

    // Password visibility toggle (nếu có icon)
    const passwordToggle = document.querySelector('.password-toggle');
    const passwordInput = document.querySelector('input[type="password"]');

    if (passwordToggle && passwordInput) {
        passwordToggle.addEventListener('click', function () {
            if (passwordInput.type === 'password') {
                passwordInput.type = 'text';
                this.innerHTML = '👁️‍🗨️';
            } else {
                passwordInput.type = 'password';
                this.innerHTML = '👁️';
            }
        });
    }

    // Thêm particles effect cho background
    createParticles();

    // Remember me animation
    const rememberCheckbox = document.querySelector('input[type="checkbox"]');
    if (rememberCheckbox) {
        rememberCheckbox.addEventListener('change', function () {
            if (this.checked) {
                this.parentElement.style.transform = 'scale(1.05)';
                setTimeout(() => {
                    this.parentElement.style.transform = 'scale(1)';
                }, 200);
            }
        });
    }
});

// Tạo particles effect
function createParticles() {
    const particlesContainer = document.createElement('div');
    particlesContainer.className = 'particles';
    particlesContainer.style.cssText = `
        position: fixed;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        pointer-events: none;
        z-index: -1;
    `;

    document.body.appendChild(particlesContainer);

    for (let i = 0; i < 50; i++) {
        const particle = document.createElement('div');
        particle.style.cssText = `
            position: absolute;
            width: 4px;
            height: 4px;
            background: rgba(255, 255, 255, 0.3);
            border-radius: 50%;
            animation: float ${3 + Math.random() * 4}s ease-in-out infinite;
            left: ${Math.random() * 100}%;
            top: ${Math.random() * 100}%;
            animation-delay: ${Math.random() * 2}s;
        `;

        particlesContainer.appendChild(particle);
    }

    // Thêm CSS animation cho particles
    const style = document.createElement('style');
    style.textContent = `
        @keyframes float {
            0%, 100% { transform: translateY(0px) rotate(0deg); opacity: 0.3; }
            50% { transform: translateY(-20px) rotate(180deg); opacity: 0.8; }
        }
        
        .btn-login.loading {
            position: relative;
            color: transparent;
        }
        
        .btn-login.loading::after {
            content: '';
            position: absolute;
            top: 50%;
            left: 50%;
            width: 20px;
            height: 20px;
            margin: -10px 0 0 -10px;
            border: 2px solid rgba(255,255,255,0.3);
            border-top: 2px solid white;
            border-radius: 50%;
            animation: spin 1s linear infinite;
        }
        
        @keyframes spin {
            0% { transform: rotate(0deg); }
            100% { transform: rotate(360deg); }
        }
    `;

    document.head.appendChild(style);
}

// Thêm keyboard shortcuts
document.addEventListener('keydown', function (e) {
    // Enter để submit form
    if (e.key === 'Enter' && e.target.tagName !== 'BUTTON') {
        const form = document.querySelector('#loginForm');
        if (form) {
            form.submit();
        }
    }
});