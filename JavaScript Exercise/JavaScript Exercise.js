// Zehua Pan 2021/04/13

// Question 1
let salaries = {
    John: 100,
    Ann: 160,
    Pete: 130
}

var sum = salaries.Ann + salaries.John + salaries.Pete


// Question 2
// before the call
function multiplyNumeric(obj) {
    for (const key in obj) {
        if (isNaN(obj[key])) {
            continue
        }
        obj[key] = obj[key] * 2
    }
}

let menu = {
    width: 200,
    height: 300,
    title: "My menu"
};

multiplyNumeric(menu);
console.log(menu)

// Question 3
function checkEmailId(str) {
    idx1 = str.indexOf('@')
    idx2 = str.indexOf('.')
    if (idx1 == -1 || idx2 == -1) {
        return false
    }
    if (idx1 + 1 < idx2) {
        return true
    }
    return false
}
console.log(checkEmailId("harrpan@.gmail.com"))

// Question 4
function truncate(str, maxlength) {
    return (str.length > maxlength) ? str.substr(0, maxlength-1) + '...' : str;
}
console.log(truncate("What I'd like to tell on this topic is:", 20))

// Question 5
styles = ["James", "Brennie"]
styles.push("Robert")
styles[Math.floor(styles.length / 2)] = "Calvin"
console.log(styles.shift())
styles.unshift("Rose", "Regal")

// Question 6
function validateCards(cardsToValidate, bannedPrefixed) {
    function validate(card, bannedPrefixed) {
        let nums = Array.from(card.split(''), Number)
        if ((nums.reduce((a, b) => a + b, 0) * 2 - nums[nums.length - 1] * 2) % 10 == nums[nums.length - 1]) {
            var isValid = true
        } else {
            var isValid = false
        }
        let isAllowed = true
        for (const prefix of bannedPrefixed) {
            if (card.startsWith(prefix)) {
                isAllowed = false
                break
            }
        }
        return {card: card, isValid:isValid, isAllowed:isAllowed}
    }
    return cardsToValidate.map(x => validate(x, bannedPrefixed))

}


console.log(validateCards(["6724843711060148"], ["11", "3434", "67453", "9", "67"]))

// Question 7
const regex = new RegExp("[a-z]{1,6}[_[1-9]{0,4}]?@hackerrank.com$")
console.log(regex.test("julia0@hackerrank.com"))