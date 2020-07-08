const ValidationService = {

    CheckValueForNullEmptyZeroLength: function (value) {
        console.log(value);

        if (value === null || value === '' || value.length === 0) {
            return false;
        }

        return true;
    },
    CheckValueForNullOnly: function (value) {
        console.log(value);

        if (value === null) {
            return false;
        }

        return true;
    },

    CheckValueForEmptyOnly: function (value) {
        console.log(value);

        if (value === '') {
            return false;
        }

        return true;
    }

};

export default ValidationService;