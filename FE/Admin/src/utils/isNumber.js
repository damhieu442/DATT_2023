const floatNumberRegex = /^(?:[1-9]\d*|0)?(?:\.\d+)?$/;

export default (number) => floatNumberRegex.test(number);
