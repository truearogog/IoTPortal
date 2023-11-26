MyDevicesJs = (function () {
    let defaults = {
        myDevicesUrl: ''
    };

    function init(parameters) {
        $.extend(defaults, parameters);
    }

    function loadMyDevices() {
        $('#devices').load(defaults.myDevicesUrl);
    }

    return {
        init,
        loadMyDevices
    };
})();