module.exports = {    
      apiUrl: process.env.NODE_ENV === 'production'
      ? '/api'
      : 'http://localhost:8085/api'
}