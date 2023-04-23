const moneyRegex = /^\$?\d+(\.\d{3})*(,\d*)?$/;

export default (number) => moneyRegex.test(number);
