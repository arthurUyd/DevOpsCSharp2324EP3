// site.js
function initializeNavbarBurger() {
    const burger = document.querySelector('.navbar-burger');
    const menu = document.getElementById(burger.getAttribute('data-target'));

    if (burger && menu) {
        burger.addEventListener('click', () => {
            burger.classList.toggle('is-active');
            menu.classList.toggle('is-active');
        });
    }
}
