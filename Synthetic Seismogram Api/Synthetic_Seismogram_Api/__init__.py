"""
The flask application package.
"""

from flask import Flask
app = Flask(__name__)

import Synthetic_Seismogram_Api.views
