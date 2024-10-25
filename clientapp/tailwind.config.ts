/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{html,ts}'],
  theme: {
    extend: {
      colors: {
        'burnt-orange': '#e66000',
        'bright-orange': '#ff9500',
        'brighter-orange': '#ffca80',
      },
      spacing: {
        'carousel-height': '32rem',
      },
    },
    container: {
      center: true,
    },
  },
  plugins: [],
};
