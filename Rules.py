class Rules(object):

    def reit(logic_val, reference):
        #evaluate logic_val for proper use of reiteration
        if len(reference) > 1:
            return False
        else
            for key in reference:
                if key == logic_val:
                    return True # If the two match, ie P = P, then correct use of Reit
            return False # Couldn't find any matches

    def orIntro(logic_val, reference):
        #evaluate logic_val for proper use of orIntro
    def orElim(logic_val, reference):
        #evaluate logic_val for proper use of orElim
    def andIntro(logic_val, reference):

    def andElim(logic_val, reference):

    def notIntro(logic_val, reference):

    def notElim(logic_val, reference):

    def contraIntro(logic_val, reference):

    def contraElim(logic_val, reference):
        if len(reference) > 1:
            return False
        else
            for val in reference.values():
                if val == '!':
                    return True # If the reference line is a contradiction then can assume anything after
            return False # The reference line wasn't a contradiction

    def impIntro(logic_val, reference):

    def impElim(logic_val, reference):

    def biIntro(logic_val, reference):

    def biElim(logic_val, reference):
