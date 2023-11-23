/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["../../EntitledLearningTools/**/*.{razor,cshtml}"],
    theme: {
      extend: {},
    },
    plugins: [
      require('@tailwindcss/forms'),
      require('@tailwindcss/typography'),
    ]
  }