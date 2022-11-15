import pandas as pd
from flask import Flask, request, jsonify, render_template
from waitress import serve
import pickle
from flask_restful import Api, Resource

# install flask, waitress, pickle into your ananconda environment

app = Flask(__name__)
api= Api(app)
modelOne = pickle.load(open('model1.pkl', 'rb')) # load model1 to the server, 'rb' - read binary
df_time_series = pd.read_pickle('model2.pkl') 



class callModelOne(Resource):
    def get(self):
        xValue = request.args.get('x', type= int)
        return {"result": str(modelOne.predict([[xValue]])[0])}


class callModelTwo(Resource):
    def get(self):
        xValue = request.args.get('x', type= int)
        return {"result": str(df_time_series[xValue])}

api.add_resource(callModelOne, "/model1", methods=['GET'])
api.add_resource(callModelTwo, "/model2", methods=['GET'])


# run the server
if __name__ == '__main__':
    print("Starting the server.....")
    serve(app, host="0.0.0.0", port=8080)
