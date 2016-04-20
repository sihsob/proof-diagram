class Rules(object):

    def reit(logic_val, reference):
        #evaluate logic_val for proper use of reiteration
        if len(reference) > 1:
            return False
        else:
            for val in reference.values():
                if val == logic_val:
                    return True # If the two match, ie P = P, then correct use of Reit
            return False # Couldn't find any matches

    def orIntro(logic_val, reference):
        #evaluate logic_val for proper use of orIntro
        #Note might have to do a stronger check to make sure that things
        #are using V's and/or grouped by parentheses
        if len(reference) > 1:
            return False
        else:
            for val in reference.values():
                if logic_val.find(val, beg=0, end=len(logic_val)) != -1:
                    return True
                else:
                    return False

    def orElim(logic_val, reference):
        #evaluate logic_val for proper use of orElim
        return False

    def andIntro(logic_val, reference):
        return False

    def andElim(logic_val, reference):
        #Note might need to do a smarter check to make sure reference line is using ANDs
        if len(reference) > 1:
            return False
        else:
            for val in reference.values():
                if val.find(logic_val, beg=0, end=len(val)) != -1:
                    return True
                else:
                    return False

    def notIntro(logic_val, reference):
        return False

    def notElim(logic_val, reference):
        if len(reference) > 1:
            return False
        else:
            for val in reference.values():
                if val == ('~~' + logic_val):
                    return True
                else:
                    return False

    def contraIntro(logic_val, reference):
        return False

    def contraElim(logic_val, reference):
        if len(reference) > 1:
            return False
        else:
            for val in reference.values():
                if val == '!':
                    return True # If the reference line is a contradiction then can assume anything after
            return False # The reference line wasn't a contradiction

    def impIntro(logic_val, reference):
        return False

    def impElim(logic_val, reference):
        return False

    def biIntro(logic_val, reference):
        return False

    def biElim(logic_val, reference):
        return False
