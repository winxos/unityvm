using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CSharpVM;
using System;
public class vm : MonoBehaviour
{
    VM v;
    public Text l_mem, l_reg;
    public InputField input;
    // Use this for initialization
    public GameObject smem, ui;
    private List<GameObject> mems;
    void Start()
    {
        v = new VM();
        mems = new List<GameObject>();
        Vector3 start_offset = smem.transform.localPosition;
        print(start_offset);
        for (int i = 0; i < v._mem.Length; i += 5)
        {
            for (int j = 0; j < 5; j++)
            {
                GameObject m = Instantiate(smem, ui.transform) as GameObject;
                m.transform.localPosition = start_offset + new Vector3(j * 110, -(i / 5) * 20, 0);
                m.transform.localScale = Vector3.one;
                m.name = "m" + (i + j);
                mems.Add(m);
            }
        }
        for (int i = 0; i < v._mem.Length / 5; i++)
        {
            GameObject m = Instantiate(smem, ui.transform) as GameObject;
            m.transform.localPosition = smem.transform.localPosition + new Vector3(-70, -i * 20, 0);
            m.transform.localScale = Vector3.one;
            m.GetComponent<Text>().text = string.Format("<color=red>0x{0:d2}</color> ", i);
        }
    }
    public void load_code()
    {
        string[] scodes = input.text.Split();
        int[] codes = new int[scodes.Length];
        for (int i = 0; i < scodes.Length; i++)
        {
            if (scodes[i].Trim() != string.Empty)
            {
                codes[i] = Convert.ToInt32(scodes[i], 16);
            }
        }
        v.load(codes);
    }
    public void step()
    {
        v.step();
    }
    public void reset()
    {
        v.init();
    }
    void vm_show()
    {
        l_reg.text = string.Format("ADDER:<color=blue>0x{0:x8}</color> PCODE:<color=blue>0x{1:d8}</color> TOTAL_INS:<color=blue>0x{2:d8}</color>", v._adder, v._pcode, v._ran_instructions);
        for (int i = 0; i < v._mem.Length; i++)
        {
            GameObject tmp = mems[i];
            if (v._pcode == i)
            {
                tmp.GetComponent<Text>().text = string.Format("<color=green>0x{0:x8}</color> ", v._mem[i]);
            }
            else
            {
                tmp.GetComponent<Text>().text = string.Format("0x{0:x8}", v._mem[i]);
            }

        }

    }
    // Update is called once per frame
    void Update()
    {
        vm_show();
    }
}
