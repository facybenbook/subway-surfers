using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class levelManager : MonoBehaviour
{
    public static levelManager Instance { set; get; }

    // level spawning
    private const float DISTANCE_BEFORE_SPAWN = 100.0f;
    private const int INITIAL_SEGMENTS = 10;
    private const int INITIAL_TRANSITION_SEGMENTS = 10;
    private const int MAX_SEGMENTS_ON_SCREEN = 15;
    public bool SHOW_COLLIDER = true; //$$

    private Transform cameraContainer;
    private int amountOfActiveSegments;
    private int continousSegments;
    private int currentSpawnZ;
    private int currentLevel;
    private int y1, y2, y3;


    //list of pieces
    public List<piece> ramps = new List<piece>();
    public List<piece> longBlocks = new List<piece>();
    public List<piece> jumps = new List<piece>();
    public List<piece> slides = new List<piece>();
    //public List<piece> skills = new List<piece>();
    [HideInInspector]
    public List<piece> pieces = new List<piece>(); // all the pieces in the pool

    //list of segments
    public List<segment> availableSegments = new List<segment>();
    public List<segment> availableTransitions = new List<segment>();
    public List<segment> specialTransitions = new List<segment>();
    [HideInInspector]
    public List<segment> segments = new List<segment>();

    private bool isMoving = false;

    private void Update()
    {
        if(currentSpawnZ - cameraContainer.position.z< DISTANCE_BEFORE_SPAWN)
        {
            GenerateSegment();
        }

        if(amountOfActiveSegments >= MAX_SEGMENTS_ON_SCREEN)
        {
            segments[amountOfActiveSegments - 1].Despawn();
            amountOfActiveSegments--;
        }
    }

    private void Awake()
    {
        Instance = this;
        cameraContainer = Camera.main.transform;
        currentSpawnZ = 0;
        currentLevel = 0;



    }

    private void Start()
    {
        for (int i = 0; i < INITIAL_SEGMENTS; i++)
            if (i < INITIAL_TRANSITION_SEGMENTS)
                SpawnTransition();
            else
                GenerateSegment();

    }

    private void GenerateSegment()
    {
        SpawnSegment();

        if(UnityEngine.Random.Range(0f, 1f) < (continousSegments * 0.25f))
        {
            //spawn transition segment
            continousSegments = 0;
            SpawnTransition();
             
        }
        else
        {
            continousSegments++;
        }
        if (continousSegments >= UnityEngine.Random.Range(1, 10))

        {
            continousSegments = 0;
            SpawnSpecialTransition();
            Debug.Log("special transition");
        }

    }

    private void SpawnSegment()
    {
        List<segment> possibleSeg = availableSegments.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = UnityEngine.Random.Range(0, possibleSeg.Count);

        segment s = GetSegment(id, false);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.length;
        amountOfActiveSegments++;
        s.Spawn();
    }

    private void SpawnTransition()
    {
        List<segment> possibleTransition = availableTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = UnityEngine.Random.Range(0, possibleTransition.Count);

        segment s = GetSegment(id, true);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.length;
        amountOfActiveSegments++;
        s.Spawn();
    }

    private void SpawnSpecialTransition()
    {
        List<segment> possibleSpecialTransition = specialTransitions.FindAll(x => x.beginY1 == y1 || x.beginY2 == y2 || x.beginY3 == y3);
        int id = UnityEngine.Random.Range(0, possibleSpecialTransition.Count);

        segment s = GetSpecialSegment(id, true);

        y1 = s.endY1;
        y2 = s.endY2;
        y3 = s.endY3;

        s.transform.SetParent(transform);
        s.transform.localPosition = Vector3.forward * currentSpawnZ;

        currentSpawnZ += s.length;
        amountOfActiveSegments++;
        s.Spawn();
    }


    public segment GetSegment(int id, bool transition)
    {
        segment s = null;
        s = segments.Find(x => x.segID == id && x.transition == transition && !x.gameObject.activeSelf);
        if(s == null)
        {
            GameObject go = Instantiate((transition) ? availableTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;
            s = go.GetComponent<segment>();
            s.segID = id;
            s.transition = transition;
            segments.Insert(0, s);
        }
        else
        {
            segments.Remove(s);
            segments.Insert(0, s);
        }
        return s;
    }

    public segment GetSpecialSegment(int id, bool transition)
    {
        segment s = null;
        s = segments.Find(x => x.segID == id && x.transition == transition && !x.gameObject.activeSelf);
        if (s == null)
        {
            GameObject go = Instantiate((transition) ? specialTransitions[id].gameObject : availableSegments[id].gameObject) as GameObject;
            s = go.GetComponent<segment>();
            s.segID = id;
            s.transition = transition;
            segments.Insert(0, s);
        }
        else
        {
            segments.Remove(s);
            segments.Insert(0, s);
        }
        return s;
    }

    public piece GetPiece(PieceType pt, int visualIndex)

    {
        piece p = pieces.Find(x => x.type == pt && x.visualIndex == visualIndex && !x.gameObject.activeSelf);

        if(p == null)
        {
            GameObject go = null;
            if (pt == PieceType.ramp)
                go = ramps[visualIndex].gameObject;
            else if (pt == PieceType.longBlock)
                go = longBlocks[visualIndex].gameObject;
            else if (pt == PieceType.jump)
                go = jumps[visualIndex].gameObject;
            else if (pt == PieceType.slide)
                go = slides[visualIndex].gameObject;

            go = Instantiate(go);
            p = go.GetComponent<piece>();
        }
        return p;
    }



}
