FormsApp.filter('dollarAmount', function () {
    return function (input) {
        var isNegative = input < 0;
        var dollarInput = Math.abs(input).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
        return isNegative ? '-$' + dollarInput : '$' + dollarInput;
    };
});
FormsApp.filter('percentage', function () {
    return function (input) {
        var percent = Math.abs(input).toFixed(2);
        return percent + '%';
    };
});
FormsApp.filter('commaIfNotEmpty', function () {
    return function (input) {
        if (typeof input != 'undefined' && input.length > 0)
            input = input + ", ";        
        return input;
    };
});
FormsApp.filter('addPeriod', function () {
    return function (input) {
        if (typeof input != 'undefined' && input.length > 0) {
            if (input[input.length - 1] !== '.')
                input = input + '.';
        }
        return input;
    };
});