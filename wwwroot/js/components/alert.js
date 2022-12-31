window.addEventListener("load",function() {
    Array.from(document.getElementsByClassName('alert')).forEach(element => {
        element.onclick = function() {
            element.parentElement.style.display = 'none';
        }
    });
});
