from flask import Flask
import pyodbc
import requests
import json
import re

app = Flask(__name__)

@app.route('/Analyze/<job>/<xpyears>/<githubuser>')
def analyze(job, xpyears, githubuser):

    authPayload = {'email': 'daniel2@nagami.com', 'senha': 'Senha@123'}
    authHeaders = { 'Content-type' : 'application/json' }
    tokenRequest = requests.post('https://localhost:44369/api/auth/login', data = json.dumps(authPayload, indent = 4), verify = False, headers=authHeaders)
    authToken = tokenRequest.json()['accessToken']

    bearerToken = 'Bearer '
    bearerToken += authToken
    jobHeaders = { 'Authorization': bearerToken }
    jobAddress = 'https://localhost:44394/api/Jobs/Read?jobId='
    jobAddress += job
    jobsRequest = requests.get(jobAddress, verify = False, headers=jobHeaders)

    score = 0

    conn = pyodbc.connect('Driver={SQL Server};'
                      'Server=.\\SQLEXPRESS;'
                      'Database=RobotTR-Dev;'
                      'Trusted_Connection=yes;')

    conn2 = pyodbc.connect('Driver={SQL Server};'
                    'Server=.\\SQLEXPRESS;'
                    'Database=RobotTR-Dev;'
                    'Trusted_Connection=yes;')

    conn3 = pyodbc.connect('Driver={SQL Server};'
                    'Server=.\\SQLEXPRESS;'
                    'Database=RobotTR-Dev;'
                    'Trusted_Connection=yes;')
    
    query = 'SELECT * FROM Codes WHERE OwnerUser=\'{}\''.format(githubuser)

    cursor = conn.cursor()
    cursor.execute(query)

    cursor2 = conn2.cursor()
    cursor2.execute(query)

    cursor3 = conn3.cursor()
    cursor3.execute(query)

    xpNeeded = 0
    if (jobsRequest.json()["level"] == 0):
        xpNeeded = 3
    if (jobsRequest.json()["level"] == 1):
        xpNeeded = 9
    if (jobsRequest.json()["level"] == 2):
        xpNeeded = 10

    xpScore = (int(xpyears) * 250) / xpNeeded

    rw = CheckReservedWords(cursor)

    for atrr, value in rw.items():
        if value == True:
            score += 18.52

    fw = CheckFrameworks(cursor2, jobsRequest.json()['frameworks'])

    dp = CheckDesignPatter(cursor3)

    dpScore = (dp * 250) / 3
    if dpScore > 250:
        dpScore = 250

    finalScore = xpScore + score + fw + dpScore

    if finalScore > 1000:
        finalScore = 1000

    mensagem = "O perfil do candidato corresponde ao n&iacute;vel de J&uacute;nior."
    if finalScore > 500:
        mensagem = "O perfil do candidato corresponde ao n&iacute;vel de Pleno."
    if finalScore > 800:
        mensagem = "O perfil do candidato corresponde ao n&iacute;vel de Senior."

    returnObject = { 'Score': finalScore, 'Mensagem': mensagem }



    # return str(json.dumps(score, indent=4))
    # return str(json.dumps(rw, indent=4))

    return str(json.dumps(returnObject, indent=4))

def CheckReservedWords(cursor):

    reservedWords = {
        "rwIf" : False,
        "rwElse" : False,
        "rwFor" : False,
        "rwForeach" : False,
        "rwNew" : False,
        "rwConstructor" : False,
        "rwTry" : False,
        "rwCatch" : False,
        "rwFinally" : False,
        "rwArray" : False,
        "rwGeneric" : False,
        "rwInterface" : False,   
        "rwAbstract" : False,
        "rwSealed" : False,
        "rwPrivate" : False,
        "rwProtected" : False,
        "rwLINQ" : False,
        "rwLambda" : False,
        "rwProperties" : False,
        "rwHerança" : False,
        "rwGenericExtension" : False,
        "rwAsync" : False,
        "rwAwait" : False,
        "rwDependencyInjection": False
    }

    for i in cursor:
        if "if" in i.Content:
            reservedWords["rwIf"] = True
        if "else" in i.Content:
            reservedWords["rwElse"] = True
        if "for" in i.Content:
            reservedWords["rwFor"] = True
        if "foreach" in i.Content:
            reservedWords["rwForeach"]
        if "new" in i.Content:
            reservedWords["rwNew"] = True
        if "for" in i.Content:
            reservedWords["rwFor"] = True  
        if "constructor" in i.Content:
            reservedWords["rwConstructor"] = True
        if "try" in i.Content:
            reservedWords["rwTry"] = True
        if "catch" in i.Content:
            reservedWords["rwCatch"] = True
        if "finally" in i.Content:
            reservedWords["rwFinally"] = True
        if "array" in i.Content:
            reservedWords["rwArray"] = True
        if "<T>" in i.Content:
            reservedWords["rwGeneric"] = True   
        if "interface" in i.Content:
            reservedWords["rwInterface"] = True  
        if "abstract" in i.Content:
            reservedWords["rwAbstract"] = True
        if "sealed" in i.Content:
            reservedWords["rwSealed"] = True
        if "private" in i.Content:
            reservedWords["rwPrivate"] = True
        if "protected" in i.Content:
            reservedWords["rwProtected"] = True
        if ".ToList()" in i.Content or ".Where(" in i.Content or ".Select(" in i.Content:
            reservedWords["rwLINQ"] = True
        if "() =>" in i.Content:
            reservedWords["rwLambda"] = True      
        if "{ get; " in i.Content:
            reservedWords["rwProperties"] = True
        if ":" in i.Content:
            reservedWords["rwHerança"] = True
        if "public static void" in i.Content:
            reservedWords["rwGenericExtension"] = True
        if "async" in i.Content:
            reservedWords["rwAsync"] = True
        if "await" in i.Content:
            reservedWords["rwAwait"] = True
        if ".AddSingleton" in i.Content or ".AddScoped" in i.Content or ".AddTransient" in i.Content:
            reservedWords["rwDependencyInjection"] = True


    return reservedWords

def CheckFrameworks(cursor, frameworks):
    
    frameworksObj = {
        'ASP.NETMVC': False,
        'ASP.NETCore': False,
        'EntityFramework': False,
        'Dapper': False,
        'NHibernate': False,
        'Http': False,
        'RabbitMQ': False,
        'Kafka': False,
        'Spring': False,
        'Play': False,
        'Struts': False,
        'Hibernate': False,
        'Wicket': False,
        'Spark': False,
        'GWT': False
    }
    
    test = "shinara"

    for i in cursor:
        if "Microsoft.AspNetMvc" in i.Content:
            frameworksObj["ASP.NETMVC"] = True
        if "Microsoft.AspNetCore.Mvc;" in i.Content:
            frameworksObj["ASP.NETCore"] = True
        if "using Microsoft.EntityFramework" in i.Content or "using EntityFramework" in i.Content  or "using EF6" in i.Content or "using EntityFrameworkCore" in i.Content:
            frameworksObj["EntityFramework"] = True
        if "using Dapper" in i.Content:
            frameworksObj["Dapper"] = True
        if "using NHibernate" in i.Content:
            frameworksObj["NHibernate"] = True
        if "using System.Net.Http" in i.Content:
            frameworksObj["Http"] = True
        if "using RabbitMQ.Client" in i.Content:
            frameworksObj["RabbitMQ"] = True
        if "using Confluent.Kafka" in i.Content:
            frameworksObj["Kafka"] = True
        if "com.example.springboot" in i.Content:
            frameworksObj["Spring"] = True
        if "play" in i.Content:
            frameworksObj["Play"] = True
        if "org.apache.struts.action" in i.Content:
            frameworksObj["Struts"] = True
        if "org.hibernate.jpa.HibernatePersistenceProvider" in i.Content:
            frameworksObj["Hibernate"] = True
        if "org.apache.wicket" in i.Content:
            frameworksObj["Wicket"] = True
        if "spark.Spark" in i.Content:
            frameworksObj["Spark"] = True
        if "com.google.gwt" in i.Content:
            frameworksObj["GWT"] = True

    unit = 250 / len(frameworks)

    score = 0

    for fram in frameworks:
        if fram == 0 and frameworksObj["ASP.NETMVC"] == True:
            score += unit
        if fram == 1 and frameworksObj["ASP.NETCore"] == True:
            score += unit
        if fram == 2 and frameworksObj["EntityFramework"] == True:
            score += unit
        if fram == 3 and frameworksObj["Dapper"] == True:
            score += unit
        if fram == 4 and frameworksObj["NHibernate"] == True:
            score += unit
        if fram == 5 and frameworksObj["Http"] == True:
            score += unit
        if fram == 6 and frameworksObj["RabbitMQ"] == True:
            score += unit
        if fram == 7 and frameworksObj["Kafka"] == True:
            score += unit
        if fram == 8 and frameworksObj["Spring"] == True:
            score += unit
        if fram == 9 and frameworksObj["Play"] == True:
            score += unit
        if fram == 10 and frameworksObj["Struts"] == True:
            score += unit
        if fram == 11 and frameworksObj["Hibernate"] == True:
            score += unit
        if fram == 12 and frameworksObj["Wicket"] == True:
            score += unit
        if fram == 13 and frameworksObj["Spark"] == True:
            score += unit      
        if fram == 14 and frameworksObj["GWT"] == True:
            score += unit                                                                                                                                                           

    return score

def CheckDesignPatter(cursor):
    
    dp = {
        "Factory": False,
        "Builder": False,
        "Singleton": False,
        "AbstractFactory": False,
        "Prototype": False,
        "Adapter": False,
        "Bridge": False,
        "Composite": False,
        "Decorator": False,
        "Facade": False,
        "FlyWeight": False,
        "Proxy": False,
        "ChainOfResponsability": False,
        "Command": False,
        "Iterator": False,
        "Mediator": False,
        "Observer": False,
        "State": False,
        "Strategy": False,
        "TemplateMethod": False,
        "Visitor": False
    }

    designPatternCounter = 0
    
    for i in cursor:
        if "factory".lower() in i.Content.lower():
            dp["Factory"] = True
        if "builder".lower() in i.Content.lower():
            dp["Builder"] = True
        if "singleton" in i.Content:
            dp["Singleton"] = True
        if "abstractfactory".lower() in i.Content.lower():
            dp["AbstractFactory"] = True
        if "prototype".lower() in i.Content.lower():
            dp["Prototype"] = True
        if "adapter".lower() in i.Content.lower():
            dp["Adapter"] = True
        if "bridge".lower() in i.Content.lower():
            dp["Bridge"] = True
        if "composite".lower() in i.Content.lower():
            dp["Composite"] = True
        if "decorator".lower() in i.Content.lower():
            dp["Decorator"] = True                                   
        if "facade".lower() in i.Content.lower():
            dp["Facade"] = True
        if "flyweight".lower() in i.Content.lower():
            dp["FlyWeight"] = True
        if "proxy".lower() in i.Content.lower():
            dp["Proxy"] = True
        if "chain".lower() in i.Content.lower():
            dp["ChainOfResponsability"] = True
        if "command".lower() in i.Content.lower():
            dp["Command"] = True
        if "iterator".lower() in i.Content.lower():
            dp["Iterator"] = True
        if "mediatr".lower() in i.Content.lower():
            dp["Mediator"] = True
        if "memento".lower() in i.Content.lower():
            dp["Memento"] = True
        if "observer".lower() in i.Content.lower():
            dp["Observer"] = True
        if "state".lower() in i.Content.lower():
            dp["State"] = True
        if "strategy".lower() in i.Content.lower():
            dp["Strategy"] = True
        if "template".lower() in i.Content.lower():
            dp["Template"] = True
        if "visitor".lower() in i.Content.lower():
            dp["Visitor"] = True

    for atrr, value in dp.items():
        if value == True:
            designPatternCounter += 1

    return designPatternCounter