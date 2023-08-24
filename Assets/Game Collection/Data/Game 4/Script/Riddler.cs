using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering.HighDefinition;
using UnityEditorInternal;

public class Riddler : MonoBehaviour
{
    int phase = 0;
    public GameObject template;
    public float time=2f;
    GameObject puzzle,solution;
    public GameObject observeMenu, resultMenu,solveMenu;
    public AudioClip win, lose;

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
    public void Action()
    {
        if (phase < 5)
            phase++;
        else
            phase = 0;
        switch (phase)
        {
            case 1:
                {
                    Puzzle s = puzzle.GetComponent<Puzzle>();
                    s.create(Random.Range(1, 5), Random.Range(1, 5), 4, false);
                    time = 0.25f * puzzle.transform.childCount+2*s.GetColor();
                    Invoke(nameof(Action), time);
                    observeMenu.gameObject.SetActive(true);
                    observeMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Time: " + time);
                    break;
                }
            case 2:
                {
                    CancelInvoke();
                    observeMenu.gameObject.SetActive(false);
                    Puzzle puzzleScript = puzzle.GetComponent<Puzzle>();
                    puzzle.SetActive(false);
                    solution.GetComponent<Puzzle>().create(puzzleScript.sizeX, puzzleScript.sizeY, 1, true);
                    solveMenu.SetActive(true);
                    break;
                }
            case 3:
                {
                    if (Puzzle.Compare(puzzle, solution))
                    {
                        resultMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Correct!");
                        GetComponent<AudioSource>().PlayOneShot(win);
                    }
                    else
                        GetComponent<AudioSource>().PlayOneShot(lose);
                    resultMenu.SetActive(true);
                    solveMenu.SetActive(false);
                    solution.GetComponent<Puzzle>().ReadOnly = true;
                    break;
                }
            case 4:
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
                }
        }
    }
    // Start is called before the first frame update
    private void Start()
    {
        puzzle = Instantiate(template);
        puzzle.name = "Puzzle";
        solution = Instantiate(template);
        solution.name = "Solution";
        Invoke(nameof(Action), 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (observeMenu.activeSelf)
        {
             time -= Time.deltaTime;
            observeMenu.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Time: " + time.ToString("0.##"));
        }
            
    }
}
