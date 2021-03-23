# SVM API

SVM API for running Support Vector Machine Classifications.

## Installation

Use docker's [installation guide](https://docs.docker.com/get-docker/) to install docker.

## Usage

Navigate to the repo and create a python virtual environment with the requirements.txt file

For Mac:
```bash
cd SvmApi
python3 -m venv venv
. venv/bin/activate
pip install --no-cache-dir -r requirements.txt
```

Start the Flask server

```bash
flask run
```

Or Start a Docker Container

```bash
docker-compose up
```

Or Start a Docker Container after modifying it

```bash
docker-compose up --build
```

### Endpoint Information

GET /api/svc

Request body format in json
key | type
---|---
features | 2d array
labels | 1d array

Response body format in json
key | type
-|-
result | 1d array

#### Example curl request

```bash
curl ---request GET -H "Content-type: application/json" -d '{ "features": [[1, 2], [1, 2]], "labels": [1, 2] }' 'localhost:5000/api/svc'
```

#### Example curl response

```bash
{ "result": [2.0, 2.0] }
```

