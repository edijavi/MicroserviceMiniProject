node('docker') {
 
    stage 'Checkout'
        checkout scm
    stage 'Build & UnitTest'
        sh "docker build -t productapi:B${BUILD_NUMBER} -f Dockerfile ."
		sh "docker build -t orderapi:B${BUILD_NUMBER} -f Dockerfile ."
        sh "docker build -t customerapi:B${BUILD_NUMBER} -f Dockerfile ."
  
    stage 'Integration Test'
        sh "docker-compose -f docker-compose.yml up --force-recreate --abort-on-container-exit"
        sh "docker-compose -f docker-compose.yml down -v"
}