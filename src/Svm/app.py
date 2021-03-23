from flask import Flask, Response, json, request
import numpy as np
from sklearn import svm
from sklearn import preprocessing
from werkzeug.exceptions import HTTPException, InternalServerError, BadRequest

# create and configure the app
app = Flask("svm-api")

@app.route('/api/ping', methods=["GET"])
def ping():
    return "pong"

# a simple endpoint to perform svc calculations
@app.route('/api/svc', methods=["POST"])
def svc():
    body = request.get_json(force=True)

    # perform svc
    try:
        result = svc_machine(body['features'], body['labels'])
    except Exception as e:
        print(e)
        raise InternalServerError(description="An error occured while performing SVC. Check logs")

    # return svc
    result = list(map(float, result))
    response = json.dumps({
        "result": result
    })
    return Response(response=response, status=200, content_type="application/json")


# c-support vector machine function
def svc_machine(features, labels):
    features = np.array(features)
    labels = np.array(labels)

    '''
    The labels are continuous, so it needs to be encoded before training,
    and recovered the predicted categories to the original labels after prediction.
    '''

    lab_enc = preprocessing.LabelEncoder()
    encoded_labels = lab_enc.fit_transform(labels)
    clf = svm.SVC(gamma='auto')
    clf.fit(features, encoded_labels)

    predictions = clf.predict(features)
    predictions_recovered = lab_enc.inverse_transform(predictions)
    result = list(predictions_recovered)
    return result
