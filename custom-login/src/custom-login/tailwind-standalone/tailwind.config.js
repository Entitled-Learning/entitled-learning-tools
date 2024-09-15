/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["../../custom-login/**/*.{razor,chtml,html}"],
    theme: {
      extend: {},
    },
    plugins: [
      require('@tailwindcss/forms'),
      require('@tailwindcss/typography'),
    ]
  }