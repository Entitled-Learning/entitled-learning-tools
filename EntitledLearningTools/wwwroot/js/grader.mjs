import JotForm from './JotForm.js';


window.gradeForm = async (formId) => {
    if (!formId) {
        throw new Error('formId is required');
    }

    const jotForm = new JotForm('f77129a3928b5b3469fe4749f3f7b702');

    try {
        const form = await jotForm.getForm(formId);
        const formTitle = form.title;
        const questions = await jotForm.getFormQuestions(formId);
        const rubric = jotForm.extractCalcValues(questions);
        const submissions = await jotForm.getFormSubmissions(formId);
        const scores = [];

        for (const submission of submissions) {
            let studentId = '';
            const submissionData = await jotForm.getSubmission(submission.id);
            const answers = submissionData.answers;
            let score = 0;

            for (const key in answers) {
                const answer = answers[key].answer ? answers[key].answer : null;
                if (answers[key].name === 'studentId') {
                    studentId = answers[key].answer || 'Unknown';
                }
                if (rubric[key] && rubric[key][answer]) {
                    score += parseInt(rubric[key][answer], 10);
                }
            }

            score = score * 10; // Multiply the score by 10
            console.log(`(${submission.id}) Student ID: ${studentId} - Score: ${score}%`);

            scores.push({ formTitle, studentId, score });
        }

        return scores;
    } catch (error) {
        console.error(`Error: ${error.message}`);
        return [];  
    }
};

//241656538205155