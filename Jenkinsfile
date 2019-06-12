pipeline {
    agent none
    stages {
        stage('productapi') {
            agent {
                docker { image 'productapi' }
            }
            steps {
                sh 'node --version'
            }
        }
        stage('orderapi') {
            agent {
                docker { image 'orderapi' }
            }
            steps {
                sh 'node --version'
            }
        }
		stage('customerapi') {
            agent {
                docker { image 'customerapi' }
            }
            steps {
                sh 'node --version'
            }
        }
    }
}