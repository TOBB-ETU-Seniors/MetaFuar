using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : MonoBehaviour
{



    /*
     * returning response list of objects below
     {
        "items": [
            {
                "_id": {
                    "$oid": "6429b9ec8e6ecb19acd1fc98"
                },
                "name": "iphone_14_pro_max",
                "description": "Apple' s new creation",
                "price": 200,
                "images": [
                    "https://store.storeimages.cdn-apple.com/4668/as-images.apple.com/is/MX0H2?wid=1144&hei=1144&fmt=jpeg&qlt=90&.v=1567304952459",
                    "https://productimages.hepsiburada.net/s/337/375/110000115788449.jpg",
                    "https://www.trustedreviews.com/wp-content/uploads/sites/54/2019/10/iphone11promax-1-1.jpeg",
                    "https://www.apple.com/newsroom/images/tile-images/Apple_iPhone-11-Pro_Most-Powerful-Advanced_091019.jpg.og.jpg?202303301813"
                ]
            },
            {
                "_id": {
                    "$oid": "6429b9ec8e6ecb19acd1fc98"
                },
                "name": "iphone_14_pro_max",
                "description": "Apple' s new creation",
                "price": 200,
                "images": [
                    "https://store.storeimages.cdn-apple.com/4668/as-images.apple.com/is/MX0H2?wid=1144&hei=1144&fmt=jpeg&qlt=90&.v=1567304952459",
                    "https://productimages.hepsiburada.net/s/337/375/110000115788449.jpg",
                    "https://www.trustedreviews.com/wp-content/uploads/sites/54/2019/10/iphone11promax-1-1.jpeg",
                    "https://www.apple.com/newsroom/images/tile-images/Apple_iPhone-11-Pro_Most-Powerful-Advanced_091019.jpg.og.jpg?202303301813"
                ]
            }
        ],
        "user_balance": 1010
    }

     */


    public List<Urun> items { get; set; }

    public int user_balance { get; set; }

}
