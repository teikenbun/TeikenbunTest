using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;
using System;

public class Access : MonoBehaviour
{
    private MongoClient client;
    private IMongoDatabase database;
    private IMongoCollection<BsonDocument> collection;

    private Transform targetObject0;
    private Transform targetObject1;
    private Vector3 newPosition;

    private float timer = 0.0f;
    private float interval = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        client = new MongoClient("mongodb+srv://teikenbun:Aya19980112@cluster0.let7j.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0");
        database = client.GetDatabase("unity_test");
        collection = database.GetCollection<BsonDocument>("unity_test0");
        targetObject0 = GameObject.Find("Square0").transform;
        targetObject1 = GameObject.Find("Square1").transform;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= interval)
        {
            //データベースからx,y座標をsquare1に
            var result = collection.Find("{player_id:0}").FirstOrDefault();
            var resultx = result["x"];
            var resulty = result["y"];
            Debug.Log(resultx);
            double resultx0 = resultx.AsDouble;
            double resulty0 = resulty.AsDouble;
            newPosition = targetObject1.position;
            newPosition.x = (float)resultx0;
            newPosition.y = (float)resulty0;
            targetObject1.position = newPosition;
            //square0のx,y座標をデータベースに
            float x = targetObject0.position.x;
            float y = targetObject0.position.y;
            var filter = Builders<BsonDocument>.Filter.Eq("player_id", 0);
            var updatex = Builders<BsonDocument>.Update.Set("x", x);
            var updatey = Builders<BsonDocument>.Update.Set("y", y);
            collection.UpdateOne(filter, updatex);
            collection.UpdateOne(filter, updatey);
            timer = 0.0f;
        }
    }
}
