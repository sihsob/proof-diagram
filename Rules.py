class Rules(object):

    def reit(logic_val, reference):
        #evaluate logic_val for proper use of reiteration
        if len(reference) > 1:
            return False
        else:
            for val in reference:
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
            for val in reference:
                if logic_val.find(val, beg=0, end=len(logic_val)) != -1:
                    return True
                else: return False

    def orElim(logic_val, reference):
        #evaluate logic_val for proper use of orElim
        return False

    def andIntro(logic_val, reference):
        for val in reference:
            # If one of the reference lines is not found in the sentence
            # Should also check the other way to make sure that all the
            # values in the sentence have associated references
            if logic_val.find(val, beg=0, end=len(logic_val)) == -1:
                return False
        return True

    def andElim(logic_val, reference):
        # Note might need to do a smarter check to make sure reference line is using ANDs
        if len(reference) > 1:
            return False
        else:
            for val in reference:
                if val.find(logic_val, beg=0, end=len(val)) != -1:
                    return True
                else: return False

    def notIntro(logic_val, reference):
        if len(reference) > 3:
            return False

        foundSubproof = False
        premise = ''
        conclusion = ''
        for val in reference:
            if foundSubproof == False and val == 'subproof':
                foundSubproof = True
            if foundSubproof == True and premise == '' and conclusion == '':
                premise = val
            if foundSubproof == True and premise != '' and conclusion == '':
                conclusion = val

        if conclusion == '!':
            if logic_val == ('~' + premise):
                return True
            else: return False
        else: return False

    def notElim(logic_val, reference):
        if len(reference) > 1:
            return False
        else:
            if reference[0] == ('~~' + logic_val):
                    return True
            else: return False

    def contraIntro(logic_val, reference):
        if len(reference) > 3:
            return False
        else:
            setVal1 = False
            setVal2 = False
            for val in reference:
                if setVal1 == False:
                    pVal1 = val
                    setVal1 = True
                elif setVal1 == True and setVal2 == False:
                    pVal2 = val

            if pVal2 == ('~' + pVal1) and logic_val == '!':
                return True
            if pVal1 == ('~' + pVal2) and logic_val == '!':
                return True
        return False

    def contraElim(logic_val, reference):
        if len(reference) > 1:
            return False
        else:
            if reference[0] == '!':
                    return True # If the reference line is a contradiction then can assume anything after
            return False # The reference line wasn't a contradiction

    def impIntro(logic_val, reference):
        foundSubproof = False
        premise = ''
        conclusion = ''

        for val in reference:
            if foundSubproof == False and val == 'subproof':
                foundSubproof = True
            if foundSubproof == True and premise == '' and conclusion == '':
                premise = val
            if foundSubproof == True and premise != '' and conclusion == '':
                conclusion = val

        if logic_val == (premise + ' -> ' + conclusion):
            return True
        else: return False

    def impElim(logic_val, reference):
        foundSubproof = False
        premise = ''
        conclusion = ''

        for val in reference:
            if foundSubproof == False and val == 'subproof':
                foundSubproof = True
            if foundSubproof == True and premise == '' and conclusion == '':
                premise = val
            if foundSubproof == True and premise != '' and conclusion == '':
                conclusion = val

        if premise == (conclusion + ' -> ' + logic_val):
            return True
        else: return False

    def biIntro(logic_val, reference):
        foundSubproof1 = False
        premise1 = ''
        conclusion1 = ''
        foundSubproof2 = False
        premise2 = ''
        conclusion2 = ''
        for val in reference:
            if foundSubproof1 == False and val == 'subproof':
                foundSubproof1 = True
            if foundSubproof1 == True and premise1 == '' and conclusion1 == '':
                premise1 = val
            if foundSubproof1 == True and premise1 != '' and conclusion1 == '':
                conclusion1 = val
            if foundSubproof2 == False and foundSubproof1 == True and val == 'subproof':
                foundSubproof2 = True
            if foundSubproof2 == True and premise2 == '' and conclusion2 == '':
                premise2 = val
            if foundSubproof2 == True and premise2 != '' and conclusion2 == '':
                conclusion2 == val

        if premise1 == conclusion2:
            if conclusion1 == premise2:
                if logic_val == (premise1 + ' <--> ' + conclusion1):
                    return True
                else: return False
            else: return False
        else: return False

    def biElim(logic_val, reference):
        if len(reference) > 2:
            return False
        else:
            ref1 = reference[0]
            ref2 = reference[1]

            if ref1.find(ref2, beg=0, end=len(ref1)) != -1 and \
               ref1.find(logic_val, beg=0, end=len(ref1)) and \
               ref1.find('<-->', beg=0, end=len(ref1)):
                return True
            else: return False
