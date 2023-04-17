using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Urun : MonoBehaviour
{
    


    /*
     
    {
                        "_id": {
                        "$oid": "6429b9ec8e6ecb19acd1fc98"
                        },
                        "name": "Iphone 11 Pro Max",
                        "description": "Apple' s new creation",
                        "price": "200",
                        "images": [
                        "https://store.storeimages.cdn-apple.com/4668/as-images.apple.com/is/MX0H2?wid=1144&hei=1144&fmt=jpeg&qlt=90&.v=1567304952459",
                        "https://productimages.hepsiburada.net/s/337/375/110000115788449.jpg",
                        "https://www.trustedreviews.com/wp-content/uploads/sites/54/2019/10/iphone11promax-1-1.jpeg",
                        "https://www.apple.com/newsroom/images/tile-images/Apple_iPhone-11-Pro_Most-Powerful-Advanced_091019.jpg.og.jpg?202303301813"
                        ]
                    }
     
     */

    public string _id { get; set; }
    public string name { get; set; } 
    public string description{ get; set; }

    public int price { get; set; }
    public string[] images{ get; set; }
    


}
