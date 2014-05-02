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
    nodemon : {
      dev : {
        script : ["./server/index.js"],
        options : {
          cwd: "C:\\PERSO_GIT\\Trombi\\Rss\\RssTEst\\Angularss\\"
        }
      }
    }
  });

  grunt.loadNpmTasks('grunt-contrib-concat');
  grunt.loadNpmTasks('grunt-nodemon');
  // Default task(s).
  grunt.registerTask('build', ['concat']);
  grunt.registerTask('run', ['nodemon']);
  grunt.registerTask('go', ['concat','nodemon']);

};