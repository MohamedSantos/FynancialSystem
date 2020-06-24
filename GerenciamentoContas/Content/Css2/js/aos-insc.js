/* Sticky nav menu */
window.addEventListener("scroll", function(){
    var header = document.querySelector("header");
    header.classList.toggle('sticky', window.scrollY > 0);
})

/* Animação de elementos */

AOS.init({
    duration: 800,
    easing: 'slide',
    once: false
  });