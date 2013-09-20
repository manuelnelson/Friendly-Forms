FormsApp.filter('dollarAmount', function () {
    return function (input) {
        var integerInput = parseInt(input);
        return integerInput < 0 ? '-$' + Math.abs(integerInput) : '$' + Math.abs(integerInput);
    };
});