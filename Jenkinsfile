pipeline {
    agent none
    stages {
        stage('productapi') {
            agent {
                docker { image 'productapi' }
            }
            
        }
        stage('orderapi') {
            agent {
                docker { image 'orderapi' }
            }
            
        }
		stage('customerapi') {
            agent {
                docker { image 'customerapi' }
            }
            
        }
    }
}