import json
from Rules import Rules

def validate():
    json_str = ''
    parsed_j = json.dumps(json_str, indent=4, separators=(',',':'))
    print parsed_j

    line_num = parsed_j['label']
    logic_val = parsed_j['sentence']
    reason = parsed_j['justification']
    reference = parsed_j['reference']

    valid_map = {
        "REIT" : Rules.reit,
        "OR I" : Rules.orIntro,
        "OR E" : Rules.orElim,
        "AND I" : Rules.andIntro,
        "AND E" : Rules.andElim,
        "NOT I" : Rules.notIntro,
        "NOT E" : Rules.notElim,
        "CONTRA I" : Rules.contraIntro,
        "CONTRA E" : Rules.contraElim,
        "IMP I" : Rules.impIntro,
        "IMP E" : Rules.impElim,
        "BI I" : Rules.biIntro,
        "BI E" : Rules.biElim
    }

    if len(reference) == 0:
        return True
    else:
        try:
             return validation_map[reason](logic_val, reference)
        except:
            print 'UNSUPPORTED LOGIC RULE'
            return False
