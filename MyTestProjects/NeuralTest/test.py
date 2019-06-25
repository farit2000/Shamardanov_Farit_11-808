import pandas as pd

data = pd.read_csv('/Users/faritsamardanov/Documents/Shamardanov_Farit_11-808/MyTestProjects/NeuralTest/titanic/titanic.csv')
#q = data['Survived'].value_counts()
# median = data['Age'].median()
# col = data['Age'].count()
# sum = data['Age'].sum()
# print(median)
# print(col)
# print(sum/col)
result = data[data['Sex'] == 'female']['Name']
q = result.apply(lambda x: x.split(',')[1].split('.')[1].split('(')[0].split('"')[0].split(' ')[1])
print(result)
print(q)
z = q.value_counts()
val = z[1]
print(z)
# firstQ = data.value_counts()
# print(firstQ)