window.addEventListener("load",function() {
    Array.from(document.getElementsByClassName('input-number')).forEach(element => {
        element.oninput = function() {
            element.value = element.value.replace(/[^0-9.]/g, '').replace(/(\..*)\./g, '$1');
        }
    });
});
