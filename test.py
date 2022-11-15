import requests

BASE = 'http://127.0.0.1:5000/'

respone1 = requests.get(BASE + "model1?x=100")
respone2 = requests.get(BASE + "model2?x=10")
print(respone1.json())
print(respone2.json())