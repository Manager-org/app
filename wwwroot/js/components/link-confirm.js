window.addEventListener("load",function() {
    Array.from(document.getElementsByClassName('link-confirm')).forEach(element => {
        element.onclick = function() {
            return confirm('Вы уверены?')
        }
    });
});
