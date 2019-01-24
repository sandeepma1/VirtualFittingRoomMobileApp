using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCSV : MonoBehaviour
{
    List<ProductInformation> products = new List<ProductInformation>();

    private void Start()
    {
        TextAsset productInfoData = Resources.Load<TextAsset>( "ProductInfoData" );

        string[] data = productInfoData.text.Split( new char[] { '\n' } );

        for( int i = 1 ; i < data.Length - 1 ; i++ )
        {
            string[] row = data[i].Split( new char[] { ',' } );

            //if( row[1] != "")
            {
                ProductInformation p = new ProductInformation();

                int.TryParse( row[0] , out p.id );
                p.ProductName = row[1];
                int.TryParse( row[2] , out p.Price );
                p.Size = row[3];

                products.Add( p );
                
            }
            foreach( ProductInformation product in products )
            {
                //if( product.id == 1 )
                {
                    Debug.Log( product.id + "," + product.ProductName );
                }
            }
        }
    }
}

   
