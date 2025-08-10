using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*  Firewall.cs
 *  author: Mack M
 *  last edited: may 29th 2024
 *  
 *  this file is designed to act as the background for the firewall systems that will be put in place
 *  it makes use of classes to define connections and filters
 *  
 *  TODO:
 *  - ensure what variables are needed in the main classes
 *  - facilitate connections coming from an outside source, rather than random generation within (if needed)
 *  - make the Connection class more accurate to IRL connection requests
 *  - create the firewall filter class and functions to allow new connections to be found
 */ 

public class Firewall : MonoBehaviour
{
    // this is a class to facilitate more in depth interactions later if needed.
    // it could theoretically just be a string for the moment, but a class looks cooler
    public class Connection
    {
        public string source;
        public int sPort;
        public string destination;
        public string dPort;
        public string protocol;
        public int length;
        public string info;

        //constructor - gonna wait until i have the variables confirmed to do this
        public Connection()
        {
            
        }
    }

    public class Filter
    {
        public Permission permission;
        public string name;
        public string protocol;
        public string source;
        public string sPorts;
        public string destination;
        public string dPorts;
        public string program;

        //constructor - gonna wait until i have the variables confirmed to do this
        public Filter()
        {

        }
    }

    // enum for pass/block/reject. more useful to make an enum compared to a string checker.
    public enum Permission
    {
        Pass,
        Block,
        Reject
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
