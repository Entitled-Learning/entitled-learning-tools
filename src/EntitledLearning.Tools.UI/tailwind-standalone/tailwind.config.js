/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["../../EntitledLearning.Tools.UI/**/*.{razor,cshtml}"],
    theme: {
      extend: {},
    },
    plugins: [
      require('@tailwindcss/forms'),
      require('@tailwindcss/typography'),
    ]
  }