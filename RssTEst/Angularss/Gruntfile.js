module.exports = function(grunt) {

  // Project configuration.
  grunt.initConfig({
    pkg: grunt.file.readJSON('package.json'),
    concat : {
      dist : {
        src : ["js/*.js"],
        dest : 'rss.js'
      }
    },
    connect: {
      server : { 
        options : {
          port:5554,
          base: './',
          hostname:'*',
          keepalive : true,
          open : {
            target:'http://localhost:5554'
          }
      }
    }
  }});

  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-contrib-connect');
  // Default task(s).
  grunt.registerTask('build', ['concat']);
  grunt.registerTask('run', ['connect']);
  grunt.registerTask('go', ['concat','connect']);

};