
window.JsFunctions =
{
    GetElementHeightById: function (id) {
        var element = document.getElementById(id);

        return element.offsetHeight;
    },
    GetElementDimensionsById: function (id) {
        var element = document.getElementById(id);
        var viewPortOffset = element.getBoundingClientRect();
        return {
            width: element.offsetWidth,
            height: element.offsetHeight,
            top: viewPortOffset.top,
            bottom: window.innerHeight - viewPortOffset.bottom,
            left: viewPortOffset.left
        };
    },
    FocusAndSelectTextById: function (id) {
        var element = document.getElementById(id);
        element.focus();
        element.select();
    },
    Test: function () {
        console.log("test");
    }
};