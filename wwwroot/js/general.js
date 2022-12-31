window.addEventListener("load",function() {
    document.body.classList.add('loaded_hiding');
    
    window.setTimeout(function () {
        document.body.classList.add('loaded');
        document.body.classList.remove('loaded_hiding');
    }, 500);
});

window.onresize = function () {
    if (window.innerWidth <= 370) {
        document.body.classList.remove('loaded');
        document.body.classList.remove('loaded_hiding');
    } else if (!document.body.classList.contains("loaded")) {
        document.body.classList.add('loaded');
    }
};
