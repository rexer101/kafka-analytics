# Kafka Analytics

A modular, containerized real-time analytics platform using Kafka, MongoDB, EMQX (MQTT broker), and .NET-based services. This system simulates telemetry data from Tesla vehicles via MQTT, processes the data using Kafka, and stores it in a MongoDB database.

## 📦 Architecture Overview
<img width="4352" alt="Architecture Diagram SS" src="https://github.com/user-attachments/assets/4c119635-b17a-4642-baae-8f66e64d582b" />

## 🧩 Tech Stack

- **Kafka** (Bitnami)
- **Zookeeper**
- **MongoDB**
- **EMQX** (MQTT broker)
- **.NET Core** Consumer
- **Angular** Dashboard
- **Docker Compose** for orchestration

## 🚀 Getting Started

### Prerequisites

- Docker & Docker Compose installed

### Clone the repository

```bash
git clone https://github.com/rexer101/kafka-analytics.git
cd kafka-analytics
docker-compose up --build

This will spin up all services including Kafka, Zookeeper, MongoDB, EMQX, MQTT simulators, the .NET consumer service, and the Angular dashboard.

### Accessing Services
Angular Dashboard: http://localhost:4200
EMQX Dashboard: http://localhost:18083
MongoDB: Available on port 7951
Kafka:
  Internal: kafka:9092
  External: localhost:9094

### Simulated Data
The mqttx-simulate container uses mqttx-cli to simulate 50 Tesla devices sending telemetry data to the EMQX broker.

### Dashboard
The Angular-based dashboard connects to MongoDB or exposed APIs to visualize processed data from the Kafka pipeline.

### Development
Modify .NET code under /Consumer
Modify Angular dashboard under /DashBoard
