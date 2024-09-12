/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["../../EntitledLearning.Enrollment.UI/**/*.{razor,cshtml}"],
    theme: {
      extend: {},
    },
    plugins: [
      require('@tailwindcss/forms'),
      require('@tailwindcss/typography'),
    ]
  }