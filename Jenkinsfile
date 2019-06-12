node('docker') {
 
    stage 'Checkout'
        checkout scm
    stage 'Build & UnitTest'
        sh "docker build -t ${DOCKER_REGISTRY-}productapi -f Dockerfile ."
		sh "docker build -t ${DOCKER_REGISTRY-}orderapi -f Dockerfile ."
        sh "docker build -t ${DOCKER_REGISTRY-}customerapi -f Dockerfile ."
  
    stage 'Integration Test'
        sh "docker-compose -f docker-compose.yml up --force-recreate --abort-on-container-exit"
        sh "docker-compose -f docker-compose.yml down -v"
}