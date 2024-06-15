export default class JotForm {
    constructor(apiKey) {
        this.apiKey = apiKey;
        this.baseUrl = 'https://api.jotform.com';
    }

    async getForm(formId) {
        try {
            const response = await fetch(`${this.baseUrl}/form/${formId}?apiKey=${this.apiKey}`);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data = await response.json();
            return data.content;
        } catch (error) {
            console.error(`There was an error fetching the form: ${error.message}`);
        }
    }

    async getFormQuestions(formId) {
        try {
            const response = await fetch(`${this.baseUrl}/form/${formId}/questions?apiKey=${this.apiKey}`);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data = await response.json();
            return data.content;
        } catch (error) {
            console.error(`There was an error fetching the form: ${error.message}`);
        }
    }

    async getFormSubmissions(formId) {
        try {
            const response = await fetch(`${this.baseUrl}/form/${formId}/submissions?apiKey=${this.apiKey}`);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data = await response.json();
            return data.content;
        } catch (error) {
            console.error(`There was an error fetching the form: ${error.message}`);
        }
    }

    async getSubmission(submissionId) {
        try {
            const response = await fetch(`${this.baseUrl}/submission/${submissionId}?apiKey=${this.apiKey}`);
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            const data = await response.json();
            return data.content;
        } catch (error) {
            console.error(`There was an error fetching the submission: ${error.message}`);
        }
    }

    extractCalcValues(questions) {
        let result = {};

        for (let key in questions) {
            let question = questions[key];
            if (question.calcValues && question.options) {
                let calcValues = question.calcValues.split('|');
                let options = question.options.split('|');
                let optionCalcValueMap = {};

                for (let i = 0; i < options.length; i++) {
                    optionCalcValueMap[options[i]] = calcValues[i] || null;
                }

                result[key] = optionCalcValueMap;
            }
        }

        return result;
    }
}
