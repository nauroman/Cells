using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

namespace Flashunity.Cells
{
    public class BTests : MonoBehaviour
    {
        [SerializeField] Transform world;

        //       BCells bCells;

        void Awake()
        {
            Debug.Log("Awake BTests");
            /*
            bCells = cells.GetComponent<BCells>();

            bCells.AddBlock(0, 0, 0);
*/

            // bCell = cell.GetComponent<BCell>();
            //bCell.cell = new Cell(new Pos(0, 0, 0), new SortedList());
        }

        // Use this for initialization
        void Start()
        {
        }



	
        // Update is called once per frame
        void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Fire1"))
            {
                AddBlockOnFire();
            }
        }

        void AddBlockOnFire()
        {
            //Screen.width / 2

            //         Log.Add(Camera.main.transform.position.ToString());

            //            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            /*
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));


            RaycastHit hit;


            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.parent == transform)
                {
                    var bChunk = hit.transform.gameObject.GetComponent<BChunk>();

                    if (bChunk != null)
                    {
                        bChunk.AddBlock(hit.point, hit.normal, true);
                        //Log.Add(hit.ToString());

                        //Debug.DrawLine(ray.origin, ray.origin + (ray.direction * hit.distance),Color.green, 2);
                    }
                }
            }
            */

        }
    }
}