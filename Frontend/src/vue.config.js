module.exports = {
    apiUrl: !process.env.API_URL
      ? 'http://localhost:8085/ '
      : process.env.API_URL
}